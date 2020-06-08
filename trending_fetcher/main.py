import os
import json
import configparser as cp
from requests_oauthlib import OAuth1Session as session

config = cp.ConfigParser()
config.read('config.ini')

script_absdir = os.path.dirname(__file__)
dataset_reldir = os.path.join(
    script_absdir, '..', config['default']['dataset_dir'])
dataset_absdir = os.path.abspath(dataset_reldir)

assert os.path.exists(dataset_absdir) and os.path.isdir(dataset_absdir), \
    f'Directory for dataset does not exist.'


def get_twitter_session():
    api, token = config['api'], config['token']
    a_key, a_secret = api['key'], api['secret']
    t_key, t_secret = token['key'], token['secret']

    twitter = session(
        client_key=a_key,
        client_secret=a_secret,
        resource_owner_key=t_key,
        resource_owner_secret=t_secret)
    return twitter


def extract_trend(trend):
    name = trend["name"]
    volume = trend["tweet_volume"]
    return {"name": name, "volume": volume}


def fetch_trends():
    twitter = get_twitter_session()
    url = config['default']['trending_url']
    response = twitter.get(url).json()[0]

    date_created = response["created_at"][:10].replace('-', '_')
    trends = list(map(extract_trend, response["trends"][:20]))
    return (trends, date_created)


def save_trends(trends, filename):
    file_abspath = os.path.join(dataset_absdir, f'{filename}.json')
    with open(file_abspath, "wt") as f:
        json.dump(trends, f, indent=4)


if __name__ == "__main__":
    trends, date_created = fetch_trends()
    save_trends(trends, date_created)
    pass
