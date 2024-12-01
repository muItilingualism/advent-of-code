#!/bin/sh

if [ $# -eq 0 ]; then
    echo "Please provide an .fsx file as an argument."
    exit 1
fi

input_file="$1"
absolute_path=$(realpath "$input_file")

if [ ! -f "$absolute_path" ]; then
    echo "File not found: $absolute_path"
    exit 1
fi

if [[ "$absolute_path" != *.fsx ]]; then
    echo "The input file must have a .fsx extension."
    exit 1
fi

filename=$(basename "$absolute_path" .fsx)
module_name="$(tr '[:lower:]' '[:upper:]' <<< ${filename:0:1})${filename:1}"

temp_script=$(mktemp --suffix .fsx)

cat << EOF > "$temp_script"
#load "$absolute_path"
open $module_name
printfn "Module $module_name loaded successfully."
EOF

dotnet fsi --use:"$temp_script"

rm "$temp_script"

