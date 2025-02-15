package com.adventofcode2024

import java.io.File

object PartTwoPuzzle {
    fun solve(path: String): Long {
        require(path.isNotEmpty()) { "Path cannot be null or empty." }
        val lines = File(path).readLines()
        return solve(lines)
    }

    private fun solve(rows: List<String>): Long = TODO()
}
