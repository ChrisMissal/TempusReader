# TempusReader

## What is this?

TempusReader is just a project that I thought could be useful for parsing durations of time in plain text English. Currently, this is just a fun example to try out [Parsley](https://github.com/plioi/parsley), but if anybody finds it useful, I'd love some feedback!

[![Status of Dependencies](http://nugetstatus.com/tempusreader.png "Status of Dependencies")](http://nugetstatus.com/packages/tempusreader)

## Installation

Use [NuGet to install TempusReader](https://nuget.org/packages/TempusReader):

    PM> Install-Package TempusReader

## Examples

Simply create a new `Time` instance by passing in a string of text.

<!--- TimeTests start -->
### Single Values
    new Time("1 minute") // 00:01:00

    new Time("1 minutes") // 00:01:00

    new Time("0 minutes") // 00:00:00

    new Time("90 minutes") // 01:30:00

    new Time("15 min") // 00:15:00

    new Time("45 mins") // 00:45:00

    new Time("8 seconds") // 00:00:08

    new Time("1 second") // 00:00:01

    new Time("123 second") // 00:02:03

    new Time("4 s") // 00:00:04

    new Time("82 sec") // 00:01:22

    new Time("1 day") // 1.00:00:00

    new Time("2 days") // 2.00:00:00

    new Time("3 d") // 3.00:00:00

    new Time("12 hours") // 12:00:00

    new Time("3 hour") // 03:00:00

    new Time("9 hr") // 09:00:00

    new Time("21 milliseconds") // 00:00:00.0210000

    new Time("500 ms") // 00:00:00.5000000

### Multiple Values
    new Time("2 days, 7 hours, 12 mins and 52 seconds") // 2.07:12:52

    new Time("4 days and 21 minutes") // 4.00:21:00

### Fractional Values
    new Time("4:15 hrs") // 04:15:00

    new Time("2.18 seconds") // 00:00:02.1800000

    new Time("6.5 minutes") // 00:06:30

### Multiple and Fractional Values
    new Time("3:45 hours and 2.5 mins") // 03:47:30

    new Time("2.25 days, 4 hours, 90 mins") // 2.11:30:00

### Mixed Case (upper/lower) Values
    new Time("13 Hours and 14 MINs") // 13:14:00

    new Time("45 SECONDS and 50 miLLiseconds") // 00:00:45.0500000

### English Words to Values
    new Time("one hour") // 01:00:00

    new Time("two minutes") // 00:02:00

    new Time("three seconds") // 00:00:03

    new Time("four ms") // 00:00:00.0040000

    new Time("five hrs") // 05:00:00

    new Time("six mins") // 00:06:00

    new Time("seven sec") // 00:00:07

    new Time("eight milliseconds") // 00:00:00.0080000

    new Time("nine hours") // 09:00:00

    new Time("ten minutes") // 00:10:00

<!--- TimeTests end -->

A `Time` instance can be created from text and cast to a `TimeSpan` instance.

## Adding/Subtracting from a Date

<!--- DateTests start -->
### Relative Date Values
    var BaseDate = new DateTime(1982, 10, 21, 23, 40, 0);

    new Date(BaseDate, "in 10 minutes") // 10/21/1982 11:50:00 PM

    new Date(BaseDate, "4 hrs from now") // 10/22/1982 3:40:00 AM

    new Date(BaseDate, "45 seconds ago") // 10/21/1982 11:39:15 PM

    new Date(BaseDate, "yesterday") // 10/20/1982 11:40:00 PM

    new Date(BaseDate, "tomorrow") // 10/22/1982 11:40:00 PM

<!--- DateTests end -->

A `Date` instance can be created from text and added/subtracted to a `DateTime` instance.
