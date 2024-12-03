package com.adventofcode2024

import java.io.File
import kotlin.math.abs
import kotlin.math.sign

object PartOnePuzzle {
    fun solve(path: String): Long {
        require(path.isNotEmpty()) { "Path cannot be null or empty" }
        val lines = File(path).readLines()
        return solve(lines)
    }

    private fun solve(rows: List<String>): Long {
        val reports = Helpers.parse(rows)
        return reports.count { isReportSafe(it) }.toLong()
    }

    private fun <TLevels> isReportSafe(levels: TLevels): Boolean where TLevels : List<Int> {
        if (levels.size <= 1) return true

        val differences = levels.zip(levels.drop(1)) { a, b -> b - a }

        if (!isDifferenceSafe(differences[0])) return false
        if (differences.size == 1) return true

        val sign = differences[0].sign
        return differences.drop(1).all { it.sign == sign && isDifferenceSafe(it) }
    }

    private fun isDifferenceSafe(difference: Int): Boolean = abs(difference) in 1..3
}
