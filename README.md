# TempusReader

## What is this?

TempusReader is just a project that I thought could be useful for parsing times in plain text English. Currently, this is just a fun example to try out [Parsley](https://github.com/plioi/parsley), but if anybody finds it useful, I'd love some feedback!

## Examples

Simply create a new `Time` instance by passing in a string of text.

    var time1 = new Time("14 minutes");
    Console.WriteLine(time1); // => 00:14:00 

    var time2 = new Time("184 s");
    Console.WriteLine(time2); // => 00:03:04

A `Time` instance can be created from text and cast to a `TimeSpan` instance.
