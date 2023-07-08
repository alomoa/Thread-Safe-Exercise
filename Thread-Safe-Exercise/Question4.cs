public class Randomness
{
    /// <summary>
    /// Moves characters to a new random location on the console.
    /// This gets called by a multithreaded method that randomly picks a Loc from a list and moves it. But the characters keep get duplicated. But I'm locking on the Loc to make sure that while its moving another thread can't move it!
    /// </summary>
    /// <param name="state"></param>
    public Loc MoveCharacter(Loc state)
    {
        //Since the original isn't being locked, other threads can still use it. If a loc is randomly selected from a list, it's likely that a loc may be selected more than once
        //Solution: lock the loc, do we even need to lock the copy?
        // We make a copy as we're going to change X and Y in a sec and don't want to have side effects on original

        lock (state)
        {
            var copy = new Loc(state.X, state.Y, state.Value);

            lock (copy)
            {
                // erase the previous position
                WriteAt(" ", copy.X, copy.Y);
            }

            // pick a new random position
            //This operation is not locked, does it need to be?
            copy.X = new Random().Next(1000);
            copy.Y = new Random().Next(1000);

            lock (copy)
            {
                WriteAt(copy.Value, copy.X, copy.Y);
            }

            return copy;
        }
    }


    public void WriteAt(string s, int x, int y)
    {
        try
        {
            Console.SetCursorPosition(x, y);
            Console.Write(s);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.Clear();
            Console.WriteLine(e.Message);
        }
    }

    // This is the definition for the Loc class. Don't make changes to this class
    public class Loc
    {
        public Loc(int x, int y, string value)
        {
            X = x;
            Y = y;
            Value = value;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public string Value { get; set; }  // Will just be one character
    }
}