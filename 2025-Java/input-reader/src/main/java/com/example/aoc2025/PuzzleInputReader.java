package com.example.aoc2025;

import java.io.IOException;
import java.io.InputStream;
import java.nio.charset.StandardCharsets;
import java.util.List;

public class PuzzleInputReader {
    private static final String INPUT_FILE_NAME = "input.txt";

    /**
     * Read input.txt file located in the caller's resources folder
     *
     * @return a list of strings with every string representing a line in the input file
     */
    public static List<String> lines() {
        Class<?> caller = findCallingClass();
        InputStream inputStream = caller.getClassLoader().getResourceAsStream(INPUT_FILE_NAME);

        if (inputStream == null) {
            throw new RuntimeException("file not found: " + INPUT_FILE_NAME);
        }

        try (inputStream) {
            byte[] data = inputStream.readAllBytes();
            String stringData = new String(data, StandardCharsets.UTF_8);
            return stringData.lines().toList();
        } catch (IOException e) {
            throw new RuntimeException("Issue reading file: " + INPUT_FILE_NAME, e);
        }
    }

    /**
     * Find what module called the input reader
     * Then use the classpath of the caller to load input from the resources
     *
     * @return the Class object of the caller
     */
    private static Class<?> findCallingClass() {
        var stack = Thread.currentThread().getStackTrace();

        for (var element : stack) {
            String className = element.getClassName();

            try {
                var cls = Class.forName(className);

                if (!cls.equals(PuzzleInputReader.class) &&
                        !className.startsWith("java.") &&
                        !className.startsWith("jdk.")) {
                    return cls;
                }
            } catch (ClassNotFoundException e) {
                // ignore error when class cannot be loaded
            }
        }

        throw new IllegalStateException("Unable to locate caller for resource lookup");
    }


}
