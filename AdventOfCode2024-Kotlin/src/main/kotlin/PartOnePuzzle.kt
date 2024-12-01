package com.adventofcode2024

import java.io.File

object PartOnePuzzle {
    fun solve(path: String): Long {
        require(path.isNotEmpty()) { "Path cannot be null or empty" }
        val lines = File(path).readLines()
        return solve(lines)
    }

    private fun <TRows : List<String>> solve(rows: TRows): Long = TODO()
}
