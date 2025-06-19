"""
    16.25. LRU Cache.
"""

class LruCache():
    cache = {}
    use_records = []

    def __init__(self, max_size = 3):
        self.max_size = max_size
        self.lowest_used_count = self.max_size;

    def get(self, key):
        if (key != self.get_next_unused_key()): self.use_records.insert(0, key)
        return self.cache.get(key)

    def get_next_unused_key(self):
        if (len(self.use_records) > 0):
            return self.use_records[0]
        return None

    def set(self, key, value):
        if (len(self.cache) >= self.max_size):
            # Kick out the least used element.
            if (
                self.get_next_unused_key() != None
                and self.cache.get(self.get_next_unused_key()) != None
            ):
                self.cache.pop(self.get_next_unused_key())
                self.use_records.pop(0)
            else:
                self.cache.pop(list(self.cache.keys())[0])

        self.cache[key] = value

    def print(self):
        for key in self.cache:
            print(key, self.cache[key])


cache = LruCache()

cache.set('hello', 'thisurl')
cache.set('hello1', 'thisurl')
cache.set('hello2', 'thisurl')
cache.set('hello3', 'thisurl')
cache.set('hello4', 'thisurl')

cache.print()

cache.get('hello')
cache.get('hello1')
cache.get('hello1')
cache.get('hello1')
cache.get('hello1')
cache.get('hello2')
cache.get('hello3')
cache.get('hello4')

cache.set('hello5', 'thisurl')
cache.set('hello6', 'thisurl')
cache.set('hello7', 'thisurl')

cache.set('hello8', 'thisurl')
cache.set('hello9', 'thisurl')
cache.set('hello11', 'thisurl')
cache.print()