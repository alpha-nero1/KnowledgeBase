#!/usr/bin/env python
import sys
import warnings

from datetime import datetime

from coder.crew import Coder

warnings.filterwarnings("ignore", category=SyntaxWarning, module="pysbd")

assignment = "Write a python program to calculate in how many years I will be able to pay off my mortgage given \
the total amount, borrowed, weekly installments and the interes rate"

def run():
    inputs = {
        "assignment": assignment
    }

    results = Coder().crew().kickoff(inputs=inputs)
    print(results.raw)