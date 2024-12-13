package com.adventofcode2024

import java.io.File

object PartOnePuzzle {
    private val pattern = Regex("""mul\((\d{1,3}),(\d{1,3})\)""")

    fun solve(path: String): Long {
        require(path.isNotEmpty()) { "Path cannot be null or empty." }
        val lines = File(path).readLines()
        return solve(lines)
    }

    private fun solve(rows: List<String>): Long {
        return rows.sumOf { solveSingle(it) }
    }

    private fun solveSingle(line: String): Long {
        val matches = pattern.findAll(line)
        return matches.sumOf { multiply(it) }
    }

    private fun multiply(match: MatchResult): Long {
        val (leftString, rightString) = match.destructured
        val left = leftString.toLong()
        val right = rightString.toLong()
        return left * right
    }
}
