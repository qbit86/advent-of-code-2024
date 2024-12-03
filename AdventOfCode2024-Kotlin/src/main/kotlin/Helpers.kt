package com.adventofcode2024

object Helpers {
    fun parse(rows: List<String>): Array<List<Int>> = rows.map(::parseLine).toTypedArray()

    private fun parseLine(line: String): List<Int> = line.split(" ").map { it.toInt() }.toList()
}
