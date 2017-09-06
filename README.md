Skimmer Scanner: A Gas Pump Skimmer Detection App by SparkX
===========================================================

![A bluetooth based gas pump skimmer](https://cdn.sparkfun.com/assets/learn_tutorials/6/9/4/Skimmer-IC_labels.jpg)

[*The inside of a bluetooth gas pump skimmer*](https://learn.sparkfun.com/tutorials/gas-pump-skimmers)

The Skimmer Scanner is a free, open source app that detects common bluetooth based credit card skimmers predominantly found in gas pumps. The app scans for available bluetooth connections looking for a device with title **HC-05**. If found, the app will attempt to connect using the default password of 1234. Once connected, the letter 'P' will be sent. If a response of 'M' then there is a very high likelihood there is a skimmer in the bluetooth range of your phone (5 to 15 feet).

The app *does not* obtain or download data from a given skimmer nor does it report any information to local authorities.

Skimmer Scanner is free, open source, and currently available for Android available [here](https://play.google.com/store/apps/details?id=skimmerscammer.skimmerscammer).

Written by Nick Poole. Skimmer teardown and research by Nathan Seidle and Rob Reynolds at [SparkFun](http://www.sparkfun.com).

Repository Contents
-------------------

* **/src** - Source files

Documentation
--------------

* **[Gas Pump Skimmers Tutorial](https://learn.sparkfun.com/tutorials/gas-pump-skimmers)** - Read all about how these skimmers work.

License Information
-------------------

This product is _**open source**_! 

Various bits of the code have different licenses applied. Anything SparkFun wrote is beerware; if you see me (or any other SparkFun employee) at the local, and you've found our code helpful, please buy us a round!

Please use, reuse, and modify these files as you see fit. Please maintain attribution to SparkFun Electronics and release anything derivative under the same license.

Distributed as-is; no warranty is given.

- Your friends at SparkFun.
