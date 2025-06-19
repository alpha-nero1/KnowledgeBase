import enum

class EdgeTypes(enum.Enum):
    Top = 1
    Right = 2
    Bottom = 3
    Left = 4


class Edge():
    piece_id = None
    other_piece_id = None
    edge_type = None

    def __init__(self, piece_id, other_piece_id, edge_type):
        self.piece_id = piece_id
        self.other_piece_id = other_piece_id
        self.edge_type_id = edge_type


class Piece():
    edges = []
    piece_id = None

    def __init__(self, piece_id):
        self.piece_id = piece_id

    def load_edges(self, edges):
        self.edges = edges

    def get_top_edge(self):
        if (len(self.edges) > 0):
            return self.edges[0]
        return None

    def get_right_edge(self):
        if (len(self.edges) > 1):
            return self.edges[1]
        return None

    def get_bottom_edge(self):
        if (len(self.edges) > 2):
            return self.edges[2]
        return None

    def get_left_edge(self):
        if (len(self.edges) > 3):
            return self.edges[3]
        return None

    def __str__(self):
        return 'Piece ' + self.piece_id


class Jigsaw():
    width = 0
    height = 0
    
    def __init__(self, width, height):
        self.__load_grid(width, height)
    
    def __load_grid(self, width, height):
        self.grid = []
        self.width = width
        self.height = height
        piece_index = 0
        # Load all the pieces in the grid first.
        for w_index in range(0, width):
            self.grid.append([])
            for h_index in range(0, height):
                piece_index += 1
                self.__load_piece(w_index, h_index, piece_index)
        
        # Then load all their edges in.
        for w_index in range(0, width):
            for h_index in range(0, height):
                self.__setup_piece(w_index, h_index, self.grid[w_index][h_index])
                
    def __load_piece(self, width_index, height_index, piece_id):        
        new_piece = Piece(piece_id)
        self.grid[width_index].append(new_piece)
    
    def __setup_piece(self, width_index, height_index, piece):
        current_col = self.grid[width_index]
        p_above = current_col[height_index - 1].piece_id if height_index > 0 else None
        p_below = current_col[height_index + 1].piece_id if (height_index + 1) < len(current_col) else None
        p_left = self.grid[width_index - 1][height_index].piece_id if width_index > 0 else None
        p_right = self.grid[width_index + 1][height_index].piece_id if width_index < (len(self.grid) - 1) else None
        piece.load_edges([
            Edge(piece.piece_id, p_above, EdgeTypes.Top),
            Edge(piece.piece_id, p_right, EdgeTypes.Right),
            Edge(piece.piece_id, p_below, EdgeTypes.Bottom),
            Edge(piece.piece_id, p_left, EdgeTypes.Left)
        ])

    def fits_with(self, edge_one, edge_two):
        return (
            edge_one.piece_id == edge_two.other_piece_id and
            edge_two.piece_id == edge_one.other_piece_id
        )

    # Great use of modulus!!
    def __str__(self):
        ret_str = ''
        grid_width = len(self.grid)
        grid_height = len(self.grid[0])
        total_pieces = grid_width * grid_height
        h_index = 0
        for i in range(0, total_pieces):
            w_index = i % grid_width
            ret_str += '[Piece: ' + str(self.grid[w_index][h_index].piece_id) + '], '
            if (w_index == grid_width - 1): 
                h_index += 1
                ret_str += '\n'

        return ret_str
    