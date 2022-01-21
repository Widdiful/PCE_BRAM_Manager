# BRAM Manager
A simple PC Engine BRAM (Backup RAM) Manager. Use this to manage the data within PC Engine backup memory files.
These files can also be transferred to and from real hardware, detailed below, or used with emulators.

![image](https://user-images.githubusercontent.com/8471483/150586497-7533613d-6088-4f57-a0bc-535fd23ccd1f.png)


## Usage
* Open one or two BRAM files with the browse buttons, or create a new one with File > New...
* Select the save file you would like to manage.
* Use the up, down, copy and delete buttons to manage the selected save file.
  * Up/Down will move the save file up and down the list.
  * Copy will copy the selected save file to the next available slot on the other loaded BRAM file, if there is enough free space.
  * Delete will delete the selected save file, shifting other data down to make room.
* Be sure to save your changes when you're finished.

## Transferring to and from real hardware
I haven't seen an explaination of how this can be achieved on the internet, so I will explain here.
* Connect your PCE to a Super SD System 3 (Super HD System 3 Pro should also work, but I haven't tested). Insert a Tennokoe Bank into the HuCard slot.
* When booted like this, the SSDS3 will use SD:\bup\backram.bup as its internal memory. Copy or rename another file to backram.bup or edit it with this manager.
* Use Tennokoe Bank to copy BRAM banks to or from one of the four available banks.
* Either connect your PCE to hardware that supports internal storage and copy data from the bank, or take your SD card out and put it in your computer. Check backram.bup to see the new files.

It can be a bit of a hassle, but it's very convenient to be able to manage data on the PCE itself or create backups on your computer.

## Detailed contents of a BRAM file
For reference, here is a breakdown of what makes up a BRAM file. You can view them in a hex editor and make adjustments as you see fit, or use this to fix any unforseen issues with the manager.

BRAM Bank Header (16 bytes)
* Header tag (4 bytes)
  * This will always be "HUBM", I believe. Leave it alone.
* Pointer to first byte after header (2 bytes)
  * This seems to always be 00 88. Leave it alone.
* Pointer to next free byte of memory (2 bytes)
  * Tells the PCE where to write the next save file. This has to point to two bytes of zeroes, or else you may experience errors.
  * An easy way to understand this is to swap the two bytes around and subtract 0x8000 (as it is being offset when loaded into the PCE's memory). For example, "23 81" becomes "0x8123", subtract 0x8000 to make 0x0123, signifying that byte 0x0123 is the start of the next slot.
  * If byte number 123 (291st byte in decimal) is the next slot, you will want these two bytes to read "23 81". If you have errors loading the memory, this being wrong is most likely why.
* Zeroes (8 bytes)
  * Blank space, leave it alone.

BRAM Entry Header (16 bytes)
* Length of entry in bytes (2 bytes)
  * This includes the 16 bytes of the header. You can use this to find where the start of the next slot will be.
  * For the last slot, it will also let you know when the next free byte of memory is if you need to edit the header.
* Checksum (2 bytes)
  * I'm not clever enough to understand this one but thankfully you can just ignore it unless you're developing your own game that uses save files in which case you're much smarter than me and probably understand it.
* ID (2 bytes)
  * Seems to always be 00 00. Not a very useful ID, it seems.
* File name (10 bytes)
  * The name of the save file. Editing this will make the save unreadable by the game, but it helps identify the data.

BRAM Entry Data
* This is the save data itself for as long as the length in the header said it would be (minus 16 bytes as it includes the header). Could be fun to mess around with if you're into that. Some games will use one chunk of data for multiple save slots.

Afterwards, the very next byte will either be the start of the next entry (another entry header followed by the data) or "00 00" to indicate the end. This first byte is the start of the next available slot.

This program will of course manage all of this for you, but I'm putting this information out there for anyone who's curious.
