package com.adventofcode2024

fun main() {
    val answer: Long = solveSingle()
    println(answer)
}

private fun solveSingle(): Long {
    val path = "../assets/input.txt"
    return try {
        PartTwoPuzzle.solve(path)
    } catch (e: NotImplementedError) {
        PartOnePuzzle.solve(path)
    }
}
