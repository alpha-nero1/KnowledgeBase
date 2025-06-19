from random import randint
from pieces import Bomb, Piece
import time

# Controls how often a piece would result in a bomb.
CHANCE_OF_BOMB = 0.1

  
# Board class definition.
class Board():
    def __init__(self, size):
        self.locations = [[None] * size] * size
        self.__initialised = False
        self.__is_bomb_counting = False
        self.__bombs = []
        self.__size = size
        self.__initialise()

    # Check coordinates are valid prior to access.
    def __valid_coordinates(self, x, y):
        return (
            x >= 0 
            and x < self.__size
            and y >= 0
            and y < self.__size
        )

    # Set up the board with the bombs
    def __initialise(self):
        mid = int(self.__size / 2)
        # - Circle the mid location to process all posible locations on the board.
        self.__circle_location(mid, mid, 1, mid + 1)
        self.__initialised = True
        # - Add all the bomb values on the board.
        self.__add_bomb_values()
        self.__is_bomb_counting = False
        print('initialised')
        print(self.__str__())

    # Initialise a piece.
    def __init_piece(self, x, y):
        is_bomb = (randint(0, 100) < (100 * CHANCE_OF_BOMB))
        if (is_bomb):
            # - Add the bomb to the board and keep a ref to it.
            bomb = Bomb(x, y)
            self.locations[x][y] = bomb
            self.__bombs.append(bomb)
        else:
            # - Initialise a piece that does not know of any bombs yet.
            self.locations[x][y] = Piece(x, y, 0)
        
        print('added piece', x, y, is_bomb)

    # Add the bomb values to pieces on the board.
    def __add_bomb_values(self):
        for bomb in self.__bombs:
            self.__circle_location(bomb.x, bomb.y, 1, 2)

    # Check a postion.
    def __check_pos(self, x, y):
        # - Check coordinate is valid before processing it.
        if (not self.__valid_coordinates(x, y)): return
        # - Setup piece if not initialised yet.
        if (not self.__initialised): 
            self.__init_piece(x, y)
        # - If we are counting nearby bomb, execute that.
        elif (self.__is_bomb_counting):
            self.locations[x][y].value += 1
        else:
            # - Process the check.
            print('checked (x:' + str(x) + ', y:' + str(y) + ')')

    # Iterate over x positions and check said position.
    def __move_x(self, x, y, pos):
        ret_array = [x, y, False]
        range_step = 1 if ((x + pos) > x) else -1 
        for i in range(x , (x + pos), range_step):
            # Check status of position
            self.__check_pos(i, y)
            ret_array[0] = i
        return ret_array

    # Iterate over y positions and check said position.
    def __move_y(self, x, y, pos):
        ret_array = [x, y, False]
        range_step = 1 if ((y + pos) > y) else -1 
        for i in range(y, (y + pos), range_step):
            # Check status of position
            self.__check_pos(x, i)
            ret_array[1] = i
        return ret_array

    # Move right on x axis and check positions.
    def __move_right(self, x, y, pos):
        print('-> moving right', x, y)
        return self.__move_x(x, y, pos + 1)

    # Move down on y axis and check positions.
    def __move_down(self, x, y, pos):
        print('-> moving down', x, y)
        return self.__move_y(x, y, pos + 1)

    # Move left on x axis and check positions.
    def __move_left(self, x, y, pos):
        print('-> moving left', x, y)
        return self.__move_x(x, y, (pos * -1) - 1)

    # Move up on y axis and check positions.
    def __move_up(self, x, y, pos):
        print('-> moving up', x, y)
        return self.__move_y(x, y, (pos * -1) - 1)

    # Run the circular check around a location.
    def __circle_location(self, x, y, layer = 1, max_layer = 2):
        if (layer == max_layer): return
        print('circling ', x, y)
        # 8, 16, 24
        items_multiplier = 8
        total_layer_items = layer * items_multiplier
        # Do the circle!
        # Start at the left most top element.
        start_point_x = x + (layer * -1)
        start_point_y = y + (layer * -1)

        side_length = int(total_layer_items / 4)
        right = self.__move_right(start_point_x, start_point_y, side_length)
        down = self.__move_down(right[0], right[1], side_length)
        left = self.__move_left(down[0], down[1], side_length)
        up = self.__move_up(left[0], left[1], side_length)

        has_empty = right[2] and down[2] and left[2] and up[2]

        if (self.__initialised and not self.__is_bomb_counting):
            return self.__circle_location(x, y, layer + 1, max_layer) if has_empty else True
        return self.__circle_location(x, y, layer + 1, max_layer)

    def __str__(self):
        ret_str = ''
        for col in self.locations:
            for item in col:
                print('aa item', item.value)
                ret_str += '[Piece ' + str(item.value) + ']'
            ret_str += '\n'
        return ret_str


board = Board(8)