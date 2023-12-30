# The Josephus Problem

[![The Josephus Problem](http://img.youtube.com/vi/uCsD3ZGzMgE/0.jpg)](https://www.youtube.com/watch?v=uCsD3ZGzMgE "The Josephus Problem")

The Josephus Problem is a counting game. In the historical example, a group of individuals sit in a circle, and beginning with the person at position one, kill the person to their left until only one person remains. At that point, the remaining person is considered to have been sitting in the "safe" seat. 

Finding an answer to the Josephus Problem, then, is simply answering the question: "For *n* number of participants, which seat position is never eliminated in the Josephus sequence?"

Luckily, we don't have to worry about individually tracking the participants because there is a very simple way to find the solution. 

If you convert the number of participants into binary like so:

> 6 → 110

And then move the first digit of the binary string to the very end: 
> 110 → 101

You then have the correct answer in binary, which you can then convert back to base 10:
> 101 → 5

With this method, you can always identify the safe seat position for any number of participants as demonstrated above. With six participants, the safe seat position is five.