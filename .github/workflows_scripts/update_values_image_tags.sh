#!/bin/bash

# Paths to the YAML configuration files
CONFIG_PATH="apps/self-hosted-runners/external-repository.yaml"
VALUES_IMAGE_PATH="apps/self-hosted-runners/values.image.yaml"

# Read the images, valuePaths, and tags from the external-repository.yaml file
IMAGES=($(yq e '.images[].name' "${CONFIG_PATH}"))
VALUE_PATHS=($(yq e '.images[].valuePath' "${CONFIG_PATH}"))
TAGS=($(yq e '.images[].tag' "${CONFIG_PATH}"))

# Loop through each image, update the values.image.yaml with the new tag according to the valuePath
for i in "${!IMAGES[@]}"; do
  VALUE_PATH="${VALUE_PATHS[$i]}"
  TAG="${TAGS[$i]}"

  # Update the values.image.yaml file with the new tag
  yq e -i ".${VALUE_PATH} = \"${TAG}\"" "${VALUES_IMAGE_PATH}"
done

# Check if any changes were made
if git diff --quiet "${VALUES_IMAGE_PATH}"; then
  echo "No changes were made to the values.image.yaml file"
else
  echo "The values.image.yaml file was updated with new tags"
fi
