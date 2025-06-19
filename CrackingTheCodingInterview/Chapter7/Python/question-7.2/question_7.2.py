from call_center import CallCenter
from employee import Respondant, Manager, Director

employees = [
  Respondant('Jason'),
  Respondant('Tiffany'),
  Respondant('Stevenson'),
  Respondant('Alec'),
  Respondant('Keith'),
  Manager('Rajiv'),
  Manager('Kasandra'),
  Manager('Paul'),
  Director('Stephen'),
  Director('Saxon')
]

call_center = CallCenter(employees)

call_center.dispatch_call()
call_center.dispatch_call()
call_center.dispatch_call()
call_center.dispatch_call()

print(call_center)