class Account:
    def __init__(self, account_id: str, initial_deposit: float):
        self.account_id = account_id
        self.balance = initial_deposit
        self.initial_deposit = initial_deposit
        self.holdings = {}
        self.transactions = []

    def deposit(self, amount: float) -> bool:
        if amount <= 0:
            return False
        self.balance += amount
        self.transactions.append({'type': 'deposit', 'amount': amount})
        return True

    def withdraw(self, amount: float) -> bool:
        if 0 < amount <= self.balance:
            self.balance -= amount
            self.transactions.append({'type': 'withdraw', 'amount': amount})
            return True
        return False

    def buy_shares(self, symbol: str, quantity: int) -> bool:
        if quantity <= 0:
            return False
        share_price = get_share_price(symbol)
        total_cost = share_price * quantity
        if total_cost <= self.balance:
            self.balance -= total_cost
            if symbol in self.holdings:
                self.holdings[symbol] += quantity
            else:
                self.holdings[symbol] = quantity
            self.transactions.append({'type': 'buy', 'symbol': symbol, 'quantity': quantity, 'price': share_price})
            return True
        return False

    def sell_shares(self, symbol: str, quantity: int) -> bool:
        if quantity <= 0 or self.holdings.get(symbol, 0) < quantity:
            return False
        share_price = get_share_price(symbol)
        total_value = share_price * quantity
        self.balance += total_value
        self.holdings[symbol] -= quantity
        if self.holdings[symbol] == 0:
            del self.holdings[symbol]
        self.transactions.append({'type': 'sell', 'symbol': symbol, 'quantity': quantity, 'price': share_price})
        return True

    def get_portfolio_value(self) -> float:
        total_value = self.balance
        for symbol, quantity in self.holdings.items():
            total_value += get_share_price(symbol) * quantity
        return total_value

    def get_profit_or_loss(self) -> float:
        return self.get_portfolio_value() - self.initial_deposit

    def get_holdings(self) -> dict:
        return self.holdings.copy()

    def get_transaction_history(self) -> list:
        return list(self.transactions)


def get_share_price(symbol: str) -> float:
    prices = {'AAPL': 150.0, 'TSLA': 600.0, 'GOOGL': 2800.0}
    return prices.get(symbol, 0.0)


if __name__ == '__main__':
    account = Account('user123', 1000.0)
    account.deposit(500.0)
    account.withdraw(200.0)
    account.buy_shares('AAPL', 5)
    account.sell_shares('AAPL', 2)
    print('Portfolio Value:', account.get_portfolio_value())
    print('Profit or Loss:', account.get_profit_or_loss())
    print('Holdings:', account.get_holdings())
    print('Transaction History:', account.get_transaction_history())