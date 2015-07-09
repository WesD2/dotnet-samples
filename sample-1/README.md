# Instructions

Create the following two console (command-line) applications. Please include your source code (solution and project
files) and a compiled binary of your application. You can develop in Visual Studio or Xamarin Studio.

------

### isPangram.exe

This app should determine whether a sentence is a pangram.

    > ./isPangram.exe the quick brown fox jumps over the lazy dog
    > Yes
    > ./isPangram.exe this sentence has many letters but is not a pangram
    > No

(If you use Xamarin Studio and mono, the commands will be `mono ./isPangram.exe ...`)

etc.

------

### sortWords.exe

This app should sort a sentence's words by their length and output a new "sentence" with the words arranged in order.

    > sortWords.exe the quick brown fox jumps over the lazy dog
    > the fox the dog over lazy quick brown jumps
    > sortWords.exe one two three four five six seven eight nine ten
    > one two six ten four five nine three seven eight

etc.
