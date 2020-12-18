const input = require('fs')
    .readFileSync('./input.txt')
    .toString()
    .split('\n')
    .map(Number)
    .reduce((accumulator, number) =>
        accumulator + (Math.floor(number / 3) - 2), 0)

console.log(input)
