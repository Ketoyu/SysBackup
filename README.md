(c) At this time, all right reserved.

This solution references the following DLLs from [QuodLib](https://github.com/Ketoyu/QuodLib), which you'll likely have to re-link upon download:
- QuodLib.IO
- QuodLib.Strings
- QuodLib.DateTimes
- QuodLib.Linq
- QuodLib.WinForms
- QuodLib.WinForms.Drawing
- QuodLib.WinForms.Linq

--------

A small Windows Forms program for facilitating system backup, opting for copy-if-newer if the destination file(s) already exist. Uses async/await for analysing file-size/count, copying files, and providing UI information about realtime progress.

Currently, this program must run with Administrator privileges.

--------

Testing/demostration steps:

1) Press ". . ." to browse for the destination backup directory.
2) Press "+" to add a directory for backup, "-" to skip a subdirectory during backup, "Del" to remove an item from the list.
3) (optional) Use "Ignore common path" to create only the rootmost path(s) in the destination backup directory, rather replicating the full source path *(i.e., copy `"C:\Users\myuser\Documents"` to the backup drive as simply `"D:\Documents"` by making the common path `"C:\Users\myuser"`)*.
4) "RUN BACKUP"
5) Update / write to one or more files within in the source directory
6) Run the backup again and verify that it took less time *(due to copy-if-newer functionality)* and that the files were updated in the destination directory.

Double-click an error from the list to get more information

Afterward, the program will export some error information in a couple files in the destination backup directory.

The "Bakcups" menu can be used to import/export the directories you've selected.

----

TODO features:
- add a "Cancel" button
- add a "Pause"/"Continue" button
