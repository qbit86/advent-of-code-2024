package com.adventofcode2024

import java.io.File

object PartTwoPuzzle {
    fun solve(path: String): Long {
        require(path.isNotEmpty()) { "Path cannot be null or empty" }
        val lines = File(path).readLines()
        return solve(lines)
    }

    private fun <TRows : List<String>> solve(rows: TRows): Long {
        val (leftNumbers, rightNumbers) = Helpers.parse(rows)

        val countByNumber = rightNumbers.groupingBy { it }.eachCount()
        fun singleSimilarityScore(number: Int): Long = number.toLong() * countByNumber.getOrDefault(number, 0)
        return leftNumbers.sumOf(::singleSimilarityScore)
    }
}
