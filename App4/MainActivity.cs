using Android.App;
using Android.Widget;
using Android.OS;
using Android.Bluetooth;
using Android.Content;
using System;
using System.IO;

namespace SkimmerScanner
{
    [Activity(Label = "Skimmer Scanner", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private BluetoothAdapter btAdapter;
        private Receiver receiver;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            BluetoothAdapter mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            if (mBluetoothAdapter == null)
            {
                // Device does not support Bluetooth
            }

            if (!mBluetoothAdapter.IsEnabled)
            {
                Intent enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                int REQUEST_ENABLE_BT = 0;
                StartActivityForResult(enableBtIntent, REQUEST_ENABLE_BT);
            }

            // Register for broadcasts when a device is discovered
            receiver = new Receiver(this);
            var filter = new IntentFilter(BluetoothDevice.ActionFound);
            RegisterReceiver(receiver, filter);

            // Register for broadcasts when discovery has finished
            filter = new IntentFilter(BluetoothAdapter.ActionDiscoveryFinished);
            RegisterReceiver(receiver, filter);

            // Register for broadcasts when a device tries to pair
            filter = new IntentFilter(BluetoothDevice.ActionPairingRequest);
            RegisterReceiver(receiver, filter);


            // Get the local Bluetooth adapter
            btAdapter = BluetoothAdapter.DefaultAdapter;

            var scanButton = FindViewById<Button>(Resource.Id.button1);
            scanButton.Click += (sender, e) =>
            {
                char[] blank = new char[1];
                FindViewById<TextView>(Resource.Id.textView2).SetText(blank,0,0);
                receiver.HC05Found = false;
                DoDiscovery();
            };

            var aboutButton = FindViewById<Button>(Resource.Id.button2);
            aboutButton.Click += (sender, e) =>
            {
                About(this);
            };

        }

        private void DoDiscovery()
        {

            // Indicate scanning 
            var status = FindViewById<TextView>(Resource.Id.textView2);
            status.Append("\nScanning...");

            // If we're already discovering, stop it
            if (btAdapter.IsDiscovering)
            {
                btAdapter.CancelDiscovery();
            }

            // Request discover from BluetoothAdapter
            btAdapter.StartDiscovery();
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Make sure we're not doing discovery anymore
            if (btAdapter != null)
            {
                btAdapter.CancelDiscovery();
            }

            // Unregister broadcast listeners
            UnregisterReceiver(receiver);
        }



        public class Receiver : BroadcastReceiver
        {
            Activity _view;

            public Receiver(Activity view)
            {
                _view = view;
            }

            public bool HC05Found = false;

            public override void OnReceive(Context context, Intent intent)
            {
                string action = intent.Action;

                // When discovery finds a device
                if (action == BluetoothDevice.ActionFound)
                {
                    // Get the BluetoothDevice object from the Intent
                    BluetoothDevice device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);

                    var status = _view.FindViewById<TextView>(Resource.Id.textView2);
                    status.Append("\nDevice " + device.Name + " found @ " + device.Address);
                    _view.FindViewById<ScrollView>(Resource.Id.scrollView1).FullScroll(Android.Views.FocusSearchDirection.Down);

                    if (device.Name == "HC-05") {
                        HC05Found = true;
                        PairHC(_view, device);
                    }

                    // When discovery is finished, change the Activity title
                }
                else if (action == BluetoothAdapter.ActionDiscoveryFinished)
                {
                    var status = _view.FindViewById<TextView>(Resource.Id.textView2);
                    status.Append("\nFinished Scanning...");
                    _view.FindViewById<ScrollView>(Resource.Id.scrollView1).FullScroll(Android.Views.FocusSearchDirection.Down);
                    if(HC05Found == false) { AllClear(context); }
                }
                else if (action == BluetoothDevice.ActionPairingRequest)
                {
                    BluetoothDevice device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);
                    String PIN = "1234";

                    byte[] pin = new byte[0];

                    pin = System.Text.Encoding.UTF8.GetBytes(PIN);

                    try
                    {

                        device.SetPin(pin);

                    }
                    catch (Java.Lang.IllegalAccessException)
                    {
                        YellowAlert(context);
                    }
                    catch (Java.Lang.Reflect.InvocationTargetException)
                    {
                        YellowAlert(context);
                    }
                    catch (Java.Lang.NoSuchMethodException)
                    {
                        YellowAlert(context);
                    }

                    BluetoothAdapter mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;
                    BluetoothSocket tmp = null;

                    try
                    {

                        tmp = device.CreateInsecureRfcommSocketToServiceRecord(Java.Util.UUID.FromString("00001101-0000-1000-8000-00805F9B34FB"));

                    }
                    catch (IOException)
                    {

                        var status = _view.FindViewById<TextView>(Resource.Id.textView2);
                        status.Append("\nFailed to create bluetooth socket...");
                        _view.FindViewById<ScrollView>(Resource.Id.scrollView1).FullScroll(Android.Views.FocusSearchDirection.Down);
                        YellowAlert(context);

                    }

                    BluetoothSocket mmSocket = tmp;
                    mBluetoothAdapter.CancelDiscovery();

                    try
                    {
                        // Connect to the remote device through the socket. This call blocks
                        // until it succeeds or throws an exception.
                        mmSocket.Connect();
                    }
                    catch (IOException)
                    {
                        YellowAlert(context);
                        // Unable to connect; close the socket and return.
                        try
                        {
                            mmSocket.Close();
                        }
                        catch (IOException)
                        {
                            
                        }
                        return;
                    }

                    ConfirmHC(_view, mmSocket, device, context);

                }

            }

        }


        public static void PairHC(Activity activity, BluetoothDevice device)
        {

            //Tell the user we found an HC-05
            var status = activity.FindViewById<TextView>(Resource.Id.textView2);
            status.Append("\nFound Possible Skimmer; Connecting...");
            activity.FindViewById<ScrollView>(Resource.Id.scrollView1).FullScroll(Android.Views.FocusSearchDirection.Down);

            //Connect to it
                device.CreateBond();

        }

        public static void ConfirmHC(Activity activity, BluetoothSocket socket, BluetoothDevice device, Context context)
        {
            
            //Tell the user we're talking to HC-05
            var status = activity.FindViewById<TextView>(Resource.Id.textView2);
            status.Append("\nConnected to HC-05 Suspected Skimmer; Sending \"P\"...");
            activity.FindViewById<ScrollView>(Resource.Id.scrollView1).FullScroll(Android.Views.FocusSearchDirection.Down);

            //Talk to it
            Stream mminputStream = socket.InputStream;
            Stream mmoutputStream = socket.OutputStream;

            byte[] buffer = new byte[1];

            buffer[0] = Convert.ToByte('P');

            mmoutputStream.Write(buffer, 0, 1);
            mminputStream.Read(buffer, 0, 1);

            status.Append("\nReceived " + Convert.ToChar(buffer[0]));

            if(Convert.ToChar(buffer[0]) == 'M')
            {
                context.StartActivity(new Intent(context, typeof(Alert)));
            }
            else { YellowAlert(context);  }

            var unpair = device.Class.GetMethod("removeBond", null);
            unpair.Invoke(device, null);

        }

        public static void YellowAlert(Context context)
        {

            context.StartActivity(new Intent(context, typeof(MildAlert)));

        }

        public static void AllClear(Context context)
        {

            context.StartActivity(new Intent(context, typeof(OKAlert)));

        }

        public static void About(Context context)
        {

            context.StartActivity(new Intent(context, typeof(About)));

        }



    }

}



