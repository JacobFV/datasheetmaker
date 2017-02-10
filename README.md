#Help Guide
This program "datasheetmaker" was designed to make it really easy to make data sheets for use in science class.

##0. Contents
1. Overview
2. Formatting Units
	
##1. Overview
Usually, when you use datasheetmaker, you'll want to do this:
1. Understand what you'll be doing in **the lab**
2. **Understand what information** you'll be recording at what time
3. **Edit variables** for the data sheet (Go to `Data > Edit Variables`)
4. Understand how to **distribute the recording of information accross group members** so that the time it takes each member to record whatever they record is about even accross the group.
4. Make **little sheets of paper with one data table on each**, one for each group member recording a column of measured data; give each sheet to each appropriate group member.
5. **Do the experiment**, while letting each group member record their data.
6. Appoint anyone as a **data-recorder**, another as a **data-reader**.
7. The data-reader will literally just **read the data** on the little pieces of paper while the data-recorder listens and **types in the data**. This way, the person typing in the data doesn't lose time looking back and forth between paper and screen. 
8. **Export** the data to staple onto the back of your lab report. (Go to `File > Export > Formatted Datasheet`)
9. **Open Excel**, then go to `File > Open` (it might also be labeled "Open Other Workbooks").
10. In the box where it says **"All Excel Files"**, click that and instead choose from the list **"All Files"**. Select the file that you exported in the last step.
11. You'll see some dialog that asks some information about how to import the data. Chose **"delimited"**, check the box labeled **"My data has headers"**, and in the box for **"File Origin"**, choose "65001: Unicode (UTF-8)"
12. **Resize the columns** in the imported data so that nothing covers over anything else.
13. **Prepare the workbook** so it will look well when printed. ([Here](https://support.office.com/en-us/article/Prepare-a-worksheet-for-printing-02103553-7B45-4871-BC7F-F005D9555AB5), [there](https://support.office.com/en-US/article/Print-rows-with-column-headers-on-top-of-every-page-D3550133-F6A1-4C72-AD70-5309A2E8FE8C), and [this](https://support.office.com/en-US/article/Scale-a-worksheet-34a91eb5-8b4e-4a8a-ab28-b6492012eaae) are some good article on formatting Excel workbooks)
14. **Print**!

##2. Formatting Units
Units are separated with period dots: `.`
Instead of saying, for example, `m/s` for meters-per-second, with datasheetmaker we say `m.s^-1`. We do this because it's less ambiguous when you do `m.kg/s/s`
