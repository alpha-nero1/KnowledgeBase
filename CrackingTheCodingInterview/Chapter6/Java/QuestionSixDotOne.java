class QuestionSixDotOne {
  public static void main(String[] args) {
    double[] bottles = {1, 1, 1, 1.1, 1, 1, 1, 1, 1};
    int heavy = QuestionSixDotOne.FindHeavyBottle(
      bottles
    );
    System.out.println("Found heavy bottle = " + heavy);
  }

  private static double round (double value, int precision) {
    int scale = (int) Math.pow(10, precision);
    return (double) Math.round(value * scale) / scale;
}

  public static int FindHeavyBottle(double[] bottles) {
    double pillTotalWeight = 0;
    double numberOfPills = 0;
    for (int i = 1; i < bottles.length; i++) {
      pillTotalWeight += bottles[i - 1] * i;
      numberOfPills += i;
    }
    double overage = QuestionSixDotOne.round((pillTotalWeight - numberOfPills), 1);
    return (int) (overage * 10);
  }
}