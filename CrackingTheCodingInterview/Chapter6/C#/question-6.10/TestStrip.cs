using System;
using System.Collections;

namespace question_6._10
{
  class TestStrip
  {
    public static int DAYS_FOR_RESULT = 7;

    private ArrayList DropsByDay = new ArrayList();

    private int Id;

    public TestStrip(int Id)
    {
      this.Id = Id;
    }

    public int GetId() { return Id; }

    private void SizeDropsForDay(int Day)
    {
      while (DropsByDay.Count <= Day)
      {
        DropsByDay.Add(new ArrayList());
      }
    }

    // Add a drop from a bottle on a specific day.
    public void AddDropOnDay(int Day, Bottle Bottle)
    {
      SizeDropsForDay(Day);
      ArrayList Drops = (ArrayList)DropsByDay[Day];
      Drops.Add(Bottle);
    }

    // Checks if any of the bottles in the set are poisoned.
    private bool HasPoison(ArrayList bottles)
    {
      foreach (Bottle b in bottles)
      {
        if (b.IsPoisoned)
        {
          return true;
        }
      }
      return false;
    }

    // Get bottles used in the test DAYS_FOR_RESULT days ago.
    public ArrayList GetLastWeeksBottles(int Day)
    {
      if (Day < DAYS_FOR_RESULT)
      {
        return null;
      }
      return (ArrayList)DropsByDay[Day - DAYS_FOR_RESULT];
    }

    // Check for poisoned bottles since before DAY_BEFORE_RESULT
    public bool IsPositiveOnDay(int Day)
    {
      int TestDay = Day - DAYS_FOR_RESULT;
      if (TestDay < 0 || TestDay >= DropsByDay.Count)
      {
        return false;
      }
      for (int d = 0; d <= TestDay; d++)
      {
        ArrayList bottles = (ArrayList)DropsByDay[d];
        if (HasPoison(bottles)) {
          return true;
        }
      }
      return false;
    }
  }
}