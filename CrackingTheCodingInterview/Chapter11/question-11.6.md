# 11.6. Test an ATM
How would you test an ATM in a distributed banking system.

We need to ask some pre-cursor questions of course:
1. Who will be using the ATMs?
2. What are they going to use them for?
3. What tools are available for testing? Do we have access to code or just the ATM (black vs white box testing)

We want to know exactly what we are testing before we jump right in!

After assumptioms and information are clarified, the next step is to define the problem into
smaller testable components. What are the key components for testing an ATM?

1. Securely logging in to the ATM.
2. Withdrawing money.
3. Depositing money.
4. Checking balance.
5. Transferring money.

We will need to implement a fair amount of automated and manual testing, the key thing here before rollout
will be to set up fake testing accounts and have a smaller network of ATMs setup that would
represent the entire network at release. Much like how we go to staging before release with software, the same should be
done for this physical network.

We need to test race conditions such as doing actions very rapidy across the network and making sure
that there are necessary locks in place and that no amounts are incorrect.
