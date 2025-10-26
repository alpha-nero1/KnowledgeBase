# 10.6 Sort Big File
Imagine you have a 20 GB file with one string per line. Explain how you would sort
the file.
&nbsp;
The main thing to first distinguish here is the size of the file, the interviewer wants you to know that you cannot
pull the full 20 gb into memory at once.
&nbsp;
The key will be to split the file into 10 2GB chunks, that when loaded into memory themselves are sorted and saved back to the file system.
Once the chunks are sorted, we can merge them one by one performing what is called an "External sort"

"""
For example, for sorting 900 megabytes of data using only 100 megabytes of RAM:

Read 100 MB of the data in main memory and sort by some conventional method, like quicksort.
Write the sorted data to disk.
Repeat steps 1 and 2 until all of the data is in sorted 100 MB chunks (there are 900MB / 100MB = 9 chunks), which now need to be merged into one single output file.
Read the first 10 MB (= 100MB / (9 chunks + 1)) of each sorted chunk into input buffers in main memory and allocate the remaining 10 MB for an output buffer. (In practice, it might provide better performance to make the output buffer larger and the input buffers slightly smaller.)
Perform a 9-way merge and store the result in the output buffer. Whenever the output buffer fills, write it to the final sorted file and empty it. Whenever any of the 9 input buffers empties, fill it with the next 10 MB of its associated 100 MB sorted chunk until no more data from the chunk is available. This is the key step that makes external merge sort work externally—because the merge algorithm only makes one pass sequentially through each of the chunks, each chunk does not have to be loaded completely; rather, sequential parts of the chunk can be loaded as needed.
"""