import json
import os
import pandas as pd

# the filename is of the format output_1_crafty_basic.lp_0.json where 1 is the rep, crafty is the configuration, basic.lp is the lpfile and 0 is the depth 
# parse a file to extract the rep, configuration, lpfile and depth
def parse_filename(filename):
    parts = filename.split('_')
    return {
        'rep': int(parts[1]),
        'configuration': parts[2],
        'lpfile': parts[3],
        'depth': int(parts[4].split('.')[0])
    }

# from this json i want to get the number of models, the total time
def parse_json_file(filename):
    with open(filename) as f:
        data = json.load(f)
        return {
            'models': data['Models']['Number'],
            'total_time': data['Time']['Total'],
            'first_time': data['Time']['Model']
        }

# let data be a panda file that stores the results
data = []


# iterate over the files in the results folder
for filename in os.listdir('results'):
    if filename.endswith('.json'):
        file = os.path.join('results', filename)
        print(f"Processing {file}")
        parsed = parse_filename(filename)
        json_data = parse_json_file(file)
        # combine parsed and data into a single dictionary
        result = {**parsed, **json_data}
        # store the result in data
        data.append(result)
        x42 = 12 

# create a dataframe from the data
df = pd.DataFrame(data)

# save the dataframe to a xlsx file
df.to_excel('results.xlsx')

 