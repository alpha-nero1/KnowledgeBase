# Piece values have a value of how many adjacend bombs there are.
class Piece:
    def __init__(self, x, y, value):
        self.value = value
        # - Store the coords for later checks/processing around these values.
        self.x = x
        self.y = y


# Bomb pieces have a value of -1
class Bomb(Piece):
    def __init__(self, x, y):
        super().__init__(x, y, -1)
        print('bomb at: ', x, y)