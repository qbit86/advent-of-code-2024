package com.adventofcode2024

import java.io.File

object PartOnePuzzle {
    fun solve(path: String): Long {
        require(path.isNotEmpty()) { "Path cannot be null or empty." }
        val line = File(path).readText().trimEnd()
        return Helpers.solve(line, 25)
    }
}
