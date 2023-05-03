import yaml
import sys
import os

config_path = os.path.join(os.environ['GITHUB_WORKSPACE'], 'apps', 'self-hosted-runners', 'external-repository.yaml')
values_image_path = os.path.join(os.environ['GITHUB_WORKSPACE'], 'apps', 'self-hosted-runners', 'values.image.yaml')

with open(config_path, 'r') as config_file:
    config_data = yaml.safe_load(config_file)

with open(values_image_path, 'r') as values_image_file:
    values_image_data = yaml.safe_load(values_image_file)

for image in config_data['images']:
    value_path_parts = image['valuePath'].split(':')
    current_node = values_image_data
    for part in value_path_parts[:-1]:
        current_node = current_node[part]
    current_node[value_path_parts[-1]] = image['tag']

with open(values_image_path, 'w') as values_image_file:
    yaml.safe_dump(values_image_data, values_image_file)
