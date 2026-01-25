import unittest
from unittest.mock import patch
from accounts import Account, get_share_price

class TestAccount(unittest.TestCase):

    def setUp(self):
        self.account = Account('test_id', 1000.0)

    def test_initial_deposit(self):
        self.assertEqual(self.account.balance, 1000.0)
        self.assertEqual(self.account.initial_deposit, 1000.0)

    def test_deposit(self):
        self.assertTrue(self.account.deposit(500.0))
        self.assertEqual(self.account.balance, 1500.0)
        self.assertIn({'type': 'deposit', 'amount': 500.0}, self.account.transactions)

    def test_withdraw(self):
        self.assertTrue(self.account.withdraw(200.0))
        self.assertEqual(self.account.balance, 800.0)
        self.assertIn({'type': 'withdraw', 'amount': 200.0}, self.account.transactions)
        self.assertFalse(self.account.withdraw(1000.0))  # Insufficient funds

    @patch('accounts.get_share_price', return_value=150.0)
    def test_buy_shares(self, mock_get_share_price):
        self.assertTrue(self.account.buy_shares('AAPL', 5))
        self.assertEqual(self.account.balance, 250.0)  # 750 spent
        self.assertEqual(self.account.holdings, {'AAPL': 5})
        self.assertIn({'type': 'buy', 'symbol': 'AAPL', 'quantity': 5, 'price': 150.0}, self.account.transactions)

    @patch('accounts.get_share_price', return_value=150.0)
    def test_sell_shares(self, mock_get_share_price):
        self.account.holdings = {'AAPL': 5}
        self.assertTrue(self.account.sell_shares('AAPL', 3))
        self.assertEqual(self.account.balance, 1450.0)
        self.assertEqual(self.account.holdings, {'AAPL': 2})
        self.assertIn({'type': 'sell', 'symbol': 'AAPL', 'quantity': 3, 'price': 150.0}, self.account.transactions)

    @patch('accounts.get_share_price', return_value=150.0)
    def test_portfolio_value(self, mock_get_share_price):
        self.account.deposit(500.0)
        self.account.buy_shares('AAPL', 5)
        self.assertEqual(self.account.get_portfolio_value(), 1250.0)  # 750 spent + 500 remaining

    def test_profit_or_loss(self):
        self.account.deposit(500.0)
        self.account.withdraw(200.0)
        self.assertEqual(self.account.get_profit_or_loss(), 300.0)  # 500 - 200

    def test_get_holdings(self):
        self.account.holdings = {'AAPL': 5}
        self.assertEqual(self.account.get_holdings(), {'AAPL': 5})

    def test_get_transaction_history(self):
        self.account.deposit(500.0)
        self.account.withdraw(200.0)
        self.assertEqual(
            self.account.get_transaction_history(),
            [
                {'type': 'deposit', 'amount': 500.0},
                {'type': 'withdraw', 'amount': 200.0},
            ]
        )

if __name__ == '__main__':
    unittest.main()