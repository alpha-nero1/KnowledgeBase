#include <iostream>
#include <map>
#include <thread>
#include <chrono>
#include <string>
#include <iomanip>

using namespace std;

class Car
{
    public:
        string number_plate;
        string colour;


        Car(string n_plate, string c)
        {
            number_plate = n_plate;
            colour = c;
        }

        void set_entry_time(long e_time)
        {
            entry_time = e_time;
        }

        long get_entry_time()
        {
            return entry_time;
        };

    private:
        long entry_time;
};


class CarPark
{
    public:
        int parked_cars;

        CarPark(
            double base_hourly_rate, 
            double* half_hourly_multipliers,
            double after_multipliers_multiplier,
            int multipliers_length
        )
        {
            this->base_hourly_rate = base_hourly_rate;
            this->half_hourly_multipliers = half_hourly_multipliers;
            this->after_multipliers_multiplier = after_multipliers_multiplier;
            this->multipliers_length = multipliers_length;
        }

        void enter(Car* car)
        {
            cout << car->number_plate << " entering car park\n\n";
            time_t entry_time = time(0);
            // Set the entry time of the car.
            car->set_entry_time(entry_time);
            // Insert entry into map.
            _cars.insert(pair<string, Car*>(car->number_plate, car));
            // Display cars.
            display();
        }

        void exit(string number_plate)
        {
            if (_cars.count(number_plate) == 0) runtime_error("Car cannot exit car park if it was never in the car park.");
            long elapsed_time = calculate_elapsed_time(number_plate);
            charge_client(_cars[number_plate], elapsed_time);
            cout << number_plate << " spent " << elapsed_time << " parked.\n";
            _cars.erase(number_plate);
            cout << number_plate << " has exited the car park\n\n";
        }

    private:
        map<string, Car*> _cars;
        double base_hourly_rate;
        double* half_hourly_multipliers;
        double after_multipliers_multiplier;
        int multipliers_length;

        void charge_client(Car * car, long time)
        {
            double amount = 0;
            if (time < multipliers_length) 
            {
                amount = base_hourly_rate * half_hourly_multipliers[time];
            }
            else
            {
                amount = base_hourly_rate * after_multipliers_multiplier;
            }
            cout << "Client " << car->number_plate << " charged $" << setprecision (2) << fixed << amount << "\n";
        }

        long calculate_elapsed_time(string number_plate)
        {
            time_t current_time = time(0);
            cout << "current time: " << current_time << "\n";
            if (_cars.count(number_plate) == 0) return 0;
            long diff = current_time - _cars[number_plate]->get_entry_time();
            return diff;
        }

        void display()
        {
            cout << "Cars: \n";
            for(map<string, Car*>::const_iterator it = _cars.begin(); it != _cars.end(); ++it)
            {
                cout << it->first << " " << it->second->number_plate << " " << it->second->get_entry_time() << "\n";
            }
            cout << "\n\n";
        };
};

int main()
{
    // << is a bitwise operator to shift bits left, don't ask me why it is applicable here.
    cout << "Simulating parking lot \n";
    double balance[5] = {1, 1, 1.2, 1.3, 1.5};
    CarPark car_park(8.0, balance, 2.0, 5);
    string n_plate = "xyz-123";
    Car first_car(n_plate, "red");
    Car second_car("abc-123", "blue");
    Car third_car("abd-e2g", "gray");
    // & accesses the reference of the object.
    car_park.enter(&first_car);
    car_park.enter(&second_car);
    car_park.enter(&third_car);
    cout << "Time is elapsing... \n\n";
    this_thread::sleep_for (chrono::milliseconds(2000));
    car_park.exit(first_car.number_plate);
    cout << "Time is elapsing... \n\n";
    this_thread::sleep_for (chrono::milliseconds(2000));
    car_park.exit(second_car.number_plate);
    cout << "Time is elapsing... \n\n";
    this_thread::sleep_for (chrono::milliseconds(2000));
    car_park.exit(third_car.number_plate);
    return 0;
}