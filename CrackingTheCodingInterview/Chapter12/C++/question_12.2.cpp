void reverse(char* str) {
    char* end = str;
    char temp;

    if (!str) return;

    // Find the end of the string.
    while(*end) {
        ++end;
    }
    --end;

    // Swap characters from start of string with end of the string untill 
    // pointers meet in the middle.
    while (str < end) {
        // We are swapping refrences here. Imagine they are like array positions.
        // set temp to starting pointer of str
        tmp = *str;
        // the next pointer of str = end pointer of str
        *str++ = *end;
        // the next end pointer is the temp we saved before...
        *end-- = temp;
    }
}