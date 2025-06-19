/*
    12.10. Malloc
    Write an aligned malloc and free function that supports allocating memory such that the
    memory address returned is divisible by a specific power of two.
*/

void* aligned_malloc(size_t required_bytes, size t alignment) {
    // initial block
    void* pl;
    // aligned block inside initial block
    void* p2;
    int offset= alignment - 1 + sizeof(void*);
    if ((pl= (void*)malloc(required_bytes + offset)) NULL) return NULL;
    p2 = (void*)(((size_t)(pl) + offset) & =(alignment - 1));
    ((void **)p2)[-1] = pl;
    return p2;
}

void aligned_free(void *p2) {
    /* for consistency, we use the same names as aligned_malloc */
    void* pl = ((void**)p2)[-1];
    free(pl);
}