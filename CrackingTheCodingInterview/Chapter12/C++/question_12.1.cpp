#include <fstream>
#include <string>
#include <iostream>  
#include <algorithm>  

using namespace std;

void print_last_k_lines(int k, char * file_name) {
    // Read in the file...
    ifstream file;
    file.open(file_name);
    // Define our array "lines" with k spaces in it!
    string lines[k];
    int size = 0;

    // Read the file line by line into the circular array.
    while (file.peek() != EOF) {
        // The size % k is what is making the array of k items circular.
        getline(file, lines[size % k]);
        size++;
    }

    // Compute size of circular array...
    int start = size > k ? (size % k) : 0;
    int count = min(k, size); // in case the size is less than the lines we specified

    cout << "Start";
    cout << start << endl;
    cout << "Count";
    cout << count << endl;

    for (int i = 0; i < count; i++) {
        int index = (start + i) % k;
        cout << "Index: ";
        cout << index << endl;
        cout << lines[index] << endl;
    }
}

int main() {
    int k;
    cout << "How many lines would you like to read: ";
    cin >> k;
    print_last_k_lines(k, "sample.txt");
    return 0;
}