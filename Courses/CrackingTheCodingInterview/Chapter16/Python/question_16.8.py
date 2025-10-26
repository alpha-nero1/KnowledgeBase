"""
    16.8. English Int

    The trick about this algorithm is that the algorithm iterates in chunks of 000, or 0-999
    And thus each chunk is evaluated and added to an overarching list that is later joined and returned.
    Quite brilliant actually.
"""

smalls = [
        'Zero', 'One', 'Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight', 'Nine',
        'Ten', 'Eleven', 'Twelve', 'Thirteen', 'Fourteen', 'Fifteen', 'Sixteen', 'Seventeen',
        'Eighteen', 'Nineteen'
    ]
tens = ['', '', 'Twenty', 'Thirty', 'Fourty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety']
larges = ['', 'Thousand', 'Million', 'Billion']
hundred = 'Hundred'
negative = 'Negative'

def convert_chunk(num):
    parts = []
    # Hundreds place...
    if (num >= 100):
        # E.g. 123 = 2
        parts.append(smalls[int(num / 100)])
        parts.append(hundred)
        parts.append('and')
        num %= 100

    # Tens place.
    if (num >= 10 and num <= 19):
        # E.g. 23 = null
        parts.append(smalls[num])
    elif (num >= 20):
        # E.g. 20
        parts.append(tens[int(num / 10)])
        num %= 10

    # Convert ones place.
    # E.g. 3
    if (num >= 1 and num <= 9):
        parts.append(smalls[int(num)])

    return ' '.join(parts)

def english_int(num):
    if (num == 0): return '';
    if (num < 0): return f'{negative} {english_int(num * -1)}'

    parts = []
    chunk_count = 0

    while (num > 0):
        # If we divide by 1000 and the remainder is not 0
        if (num % 1000 != 0):
            # Then we want to represent a 0-999 chunk.
            # Note that the larges increments which ads the '', 'Thousand', 'Million' to the string.
            chunk = f'{convert_chunk(num % 1000)} {larges[chunk_count]}'
            # Insert the chunk.
            parts.insert(0, chunk)
        num = int(num / 1000)
        chunk_count += 1

    return ' '.join(parts)



print(english_int(1988))
print()
print(english_int(234558))