#!/bin/bash

# Run the Python script to update the values.image.yaml file
python3 ./.github/workflows_scripts/update_values_image_tags.py

# Check if any changes were made
if git diff --quiet "${GITHUB_WORKSPACE}/apps/self-hosted-runners/values.image.yaml"; then
  echo "No changes were made to the values.image.yaml file"
else
  echo "The values.image.yaml file was updated with new tags"
fi
