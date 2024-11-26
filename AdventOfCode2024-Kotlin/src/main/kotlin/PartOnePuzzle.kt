package com.adventofcode2024

import java.io.File
import java.nio.charset.StandardCharsets

object PartOnePuzzle {
    fun solve(path: String): Long {
        require(path.isNotEmpty()) { "Path cannot be null or empty" }
        val lines = File(path).readLines(StandardCharsets.UTF_8)
        return solve(lines)
    }

    private fun <TRows : List<String>> solve(rows: TRows): Long = TODO()
}
