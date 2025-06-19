from employee import Employee, EMPLOYEE_LEVEL_NAMES

# Call center implementation.
class CallCenter:

  def __init__(self, employees):
    self.employees = [None] * 3
    for emp in employees:
      if (emp and emp.level):
        emp_level_index = emp.level - 1
        if (self.employees[emp_level_index] == None):
          self.employees[emp_level_index] = []
        self.employees[emp_level_index].append(emp);

  def dispatch_call(self):
    # Check for free at each level, if failure go to next.
    for i in range(0, len(self.employees)):
      for emp in self.employees[i]:
        if (emp.is_available):
          emp.take_call()
          return
  
  def __str__(self):
    ret_str = ""
    for lvl in range(0, len(self.employees)):
      ret_str = ret_str + "\n* Level: " + EMPLOYEE_LEVEL_NAMES[lvl] + "\n"
      for emp in self.employees[lvl]:
        ret_str = ret_str + str(emp) + "\n"
    return ret_str