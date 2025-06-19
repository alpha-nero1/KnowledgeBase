"""
    16.13 Bisect Squares.
    Given two squares on a 2d plane, find a line that would cut these two squares in half.
    Assume that the top and bottom sides of the square run parrallel to the x axis.
"""

class Line():
    start = None
    end = None
    def __init__(self, start, end):
        self.start = start
        self.end = end


class Point():
    x = 0;
    y = 0;

    def __init__(self, x, y):
        self.x = x
        self.y = y


class Square():
    left = 0
    right = 0
    top = 0
    bottom = 0
    size = 0

    def __init__(self, left, right, top, bottom, size):
        self.left = left
        self.right = right
        self.top = top
        self.bottom = bottom
        self.size = size

    def middle(self):
        return Point(
            (self.left + self.right) / 2,
            (self.top + self.bottom) / 2
        )

    def extend(mid_one_pt, mid_two_pt, size):
        x_direction = -1 if mid_one_pt.x < mid_two_pt.x else 1
        y_direction = -1 if mid_one_pt.y < mid_two_pt.y else 1

        # If mid1 and mid2 have the same x value, then the slope calc will throw a
        # 0 divide exception, So we compute this case beforehand.
        if (mid_one_pt.x == mid_two_pt.x): return Point(
            mid_one_pt.x,
            (mid_one_pt.y + y_direction * size / 2)
        )

        slope = (mid_one_pt.y - mid_two_pt.y) / (mid_one_pt.x - mid_two_pt.x)

        # Calculate the slope using the equation (y1 - y2) / (x1 - x2)
        slope = abs(slope)
        if (slope == 1):
            return Point(
                mid_one_pt.x + x_direction * size / 2,
                mid_one_pt.y + y_direction * size / 2,
            )
        if (slope < 1):
            x1 = mid_one_pt.x + x_direction * size / 2
            return Point(
                x1,
                slope * (x1 - mid_one_pt.x) + mid_one_pt.y
            )
        y1 = mid_one_pt.y + y_direction * size / 2
        return Point(
            y1,
            (y1 - mid_one_pt.y) / slope + mid_one_pt.x
        )

    def cut(self, other):
        p1 = self.extend(self.middle(), other.middle(), self.size)
        p2 = self.extend(self.middle(), other.middle(), -1 * self.size)
        p3 = self.extend(other.middle(), self.middle(), other.size)
        p4 = self.extend(other.middle(), self.middle(), -1 * other.size)

        # Of the above points, find the start and end lines.
        start = p1
        end = p1
        points = [p2, p3, p4]
        i = 0
        while (i < len(points)):
            if (
                points[i].x < start.x
                or (
                    points[i].x == start.x
                    and points[i].y < start.y
                )
            ): start = points[i]
            elif (
                points[i].x > end.x
                or (
                    points[i].x == end.x
                    and points[i].y > end.y
                )
            ): end = points[i]
            i += 1
        return Line(start, end)