import gradio as gr
from accounts import Account, get_share_price

# Create an instance of the Account class
account = Account('user123', 1000.0)

# Define functions to be used in the Gradio interface
def deposit(amount):
    success = account.deposit(amount)
    return account.balance, "Deposit successful" if success else "Deposit failed"

def withdraw(amount):
    success = account.withdraw(amount)
    return account.balance, "Withdrawal successful" if success else "Withdrawal failed or insufficient funds"

def buy_shares(symbol, quantity):
    success = account.buy_shares(symbol, quantity)
    return account.get_portfolio_value(), "Shares purchased" if success else "Purchase failed or insufficient funds"

def sell_shares(symbol, quantity):
    success = account.sell_shares(symbol, quantity)
    return account.get_portfolio_value(), "Shares sold" if success else "Sale failed or insufficient holdings"

def get_holdings():
    return account.get_holdings()

def get_transactions():
    return account.get_transaction_history()

def get_profit_loss():
    return account.get_profit_or_loss()

# Create the Gradio interface

with gr.Blocks() as demo:
    gr.Markdown("## Trading Simulation Platform")
    
    with gr.Tab("Account Management"):
        deposit_amount = gr.Number(label="Deposit Amount")
        deposit_balance_output = gr.Textbox(label="Balance")
        deposit_status_output = gr.Textbox(label="Status")
        deposit_button = gr.Button("Deposit")
        deposit_button.click(deposit, deposit_amount, outputs=[deposit_balance_output, deposit_status_output])
        
        withdraw_amount = gr.Number(label="Withdrawal Amount")
        withdraw_balance_output = gr.Textbox(label="Balance")
        withdraw_status_output = gr.Textbox(label="Status")
        withdraw_button = gr.Button("Withdraw")
        withdraw_button.click(withdraw, withdraw_amount, outputs=[withdraw_balance_output, withdraw_status_output])
    
    with gr.Tab("Shares Trading"):
        symbol = gr.Dropdown(choices=["AAPL", "TSLA", "GOOGL"], label="Symbol")
        quantity = gr.Number(label="Quantity")
        
        buy_portfolio_output = gr.Textbox(label="Portfolio Value")
        buy_status_output = gr.Textbox(label="Status")
        buy_button = gr.Button("Buy Shares")
        buy_button.click(buy_shares, [symbol, quantity], [buy_portfolio_output, buy_status_output])
        
        sell_portfolio_output = gr.Textbox(label="Portfolio Value")
        sell_status_output = gr.Textbox(label="Status")
        sell_button = gr.Button("Sell Shares")
        sell_button.click(sell_shares, [symbol, quantity], [sell_portfolio_output, sell_status_output])
        
    with gr.Tab("Reports"):
        holdings_button = gr.Button("Get Holdings")
        holdings_output = gr.JSON()
        holdings_button.click(get_holdings, [], holdings_output)
        
        transactions_button = gr.Button("Get Transactions")
        transactions_output = gr.JSON()
        transactions_button.click(get_transactions, [], transactions_output)
        
        profit_loss_button = gr.Button("Get Profit/Loss")
        profit_loss_output = gr.Textbox()
        profit_loss_button.click(get_profit_loss, [], profit_loss_output)
    
demo.launch()