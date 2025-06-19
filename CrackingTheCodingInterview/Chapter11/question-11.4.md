# 11.4 No Test Tools
How would you load test a webpage without using any test tools?
&nbsp;
It is important to first gather what metrics are to be tested, these would be:
- Response time.
- Throughput (Rate of successfull message delivery).
- Resource utilisation.
- Maximum load that the system can bear.
&nbsp;
In terms of no testing tools, we could just build them ourself and test out the scenario.
&nbsp;
For instance in JS terms I could simulate 1000 users visiting a page by creating a Promise.all()
with an array of 1000 constructed in it, each promise does a get request to the page and
records it's timing and status code. If we fine that 1000 is fine then we can keep increasing and testing that.