package com.adventofcode2024

object Helpers {
    fun <TRows : List<String>> parse(rows: TRows): Pair<List<Int>, List<Int>> {
        val numberPairs = rows.map { line ->
            val parts = line.split(" ").filter { it.isNotEmpty() }
            parts[0].toInt() to parts[1].toInt()
        }

        return numberPairs.unzip()
    }
}
