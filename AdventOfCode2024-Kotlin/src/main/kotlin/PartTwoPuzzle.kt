package com.adventofcode2024

import java.io.File

object PartTwoPuzzle {
    private const val PATTERN = """(mul\((\d{1,3}),(\d{1,3})\))|(do\(\))|(don't\(\))"""

    private val conditionalMulRegex: Regex by lazy { Regex(PATTERN) }

    fun solve(path: String): Long {
        require(path.isNotEmpty()) { "Path cannot be null or empty." }
        val lines = File(path).readLines()
        return solve(lines)
    }

    private fun solve(rows: List<String>): Long {
        val instructions = ArrayList<IInstruction>()
        for (line in rows) {
            populateInstructions(line, instructions)
        }
        return solveCore(instructions)
    }

    private fun solveCore(instructions: List<IInstruction>): Long {
        var sum = 0L
        var shouldAdd = true

        for (instruction in instructions) {
            when (instruction) {
                is DoInstruction -> {
                    shouldAdd = true
                    continue
                }

                is DontInstruction -> {
                    shouldAdd = false
                    continue
                }

                is MulInstruction -> {
                    if (!shouldAdd) continue
                    val left = instruction.left.value.toLong()
                    val right = instruction.right.value.toLong()
                    sum += left * right
                }

                else -> throw Exception("Unreachable code")
            }
        }

        return sum
    }

    private fun populateInstructions(line: String, instructions: MutableList<IInstruction>) {
        val matches = conditionalMulRegex.findAll(line)

        for (match in matches) {
            when (match.value) {
                "do()" -> {
                    instructions.add(DoInstruction)
                    continue
                }

                "don't()" -> {
                    instructions.add(DontInstruction)
                    continue
                }

                else -> {
                    val groups = match.groups
                    instructions.add(MulInstruction(groups[2]!!, groups[3]!!))
                }
            }
        }
    }
}
