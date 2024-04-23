import pandas as pd
import numpy as np

def get_character_by_id(player_id: int, characters: pd.DataFrame):
    "Return the character with the given player_id. If not found, return an empty dictionary."
    character = characters.loc[characters['PlayerID'] == player_id].to_dict('records')
    if character:
        return character[0]
    else:
        return {}
    
def compare_race(raceA: str, raceB: str) -> bool:
    """
    "Asian/Pacific Islander" should match with "Asian-Pacific Islander"
    "Black" should match with "African-American"
    "White" should match with "White"
    "Latino" should match with "Latinx"
    Otherwise no match
    """
    codes = {
        "Asian/Pacific Islander": 0,
        "Asian-Pacific Islander": 0,
        "Black": 1,
        "African-American": 1,
        "White": 2,
        "Latino": 3,
        "Latinx": 3
    }
    if codes.get(raceA, np.nan) == codes.get(raceB, np.nan):
        return True
    return False

def compare_age(age_str: str, age_int: int) -> bool:
    """
    age_str is one of the following ranges:
        - 18-39
        - 40-59
        - 60+

    Return True if age_int is within the range of age_str
    """
    if age_str == "18-39" and age_int >= 18 and age_int <= 39:
        return True
    elif age_str == "40-59" and age_int >= 40 and age_int <= 59:
        return True
    elif age_str == "60+" and age_int >= 60:
        return True
    return False

def convert_age(age_int: int) -> str:
    """
    Convert age_int into age_str based on the following rules:
        - If age_int is between 18 and 39 (inclusive), return "18-39"
        - If age_int is between 40 and 59 (inclusive), return "40-59"
        - If age_int is 60 or greater, return "60+"
    """
    if age_int >= 18 and age_int <= 39:
        return "18-39"
    elif age_int >= 40 and age_int <= 59:
        return "40-59"
    elif age_int >= 60:
        return "60+"
    elif age_int < 18:
        return "Under 18"
    else:
        return ""