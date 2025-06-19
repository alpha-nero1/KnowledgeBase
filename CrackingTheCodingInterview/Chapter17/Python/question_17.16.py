"""
    17.16. Masseuse
"""

def best_minutes(massages):
    print('##### START #####')
    one_away = 0
    two_away = 0

    for i in range(len(massages) - 1, 0, -1):
        print("##### IT START #####")
        best_with = massages[i] + two_away
        best_without = one_away;
        print(f'best_with = {best_with}')
        print(f'best_without = {best_without}')
        current = max(best_with, best_without)
        print(f'current = {current}')
        two_away = one_away
        one_away = current
        print()

    return one_away

print(best_minutes([30, 15, 60, 75, 45, 15, 15, 45]))