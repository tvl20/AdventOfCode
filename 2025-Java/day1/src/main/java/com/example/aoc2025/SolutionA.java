package com.example.aoc2025;

import java.util.List;

public class SolutionA {
    private final List<String> INPUT;

    private int currentRotation = 50;
    private int pointsAtZero = 0;

    public SolutionA(List<String> INPUT) {
        this.INPUT = INPUT;
    }

    public void solve(){
        for (String rotation : INPUT){
            rotate(rotation);
        }

        System.out.println("Final rotation for solution A: ");
        System.out.println(pointsAtZero);
    }

    private void rotate(String rotation){
        System.out.print(currentRotation);
        int degrees = processDegrees(rotation);

        if (rotation.startsWith("R")){
            currentRotation += degrees;
            System.out.println(" >> " + currentRotation);
        } else if (rotation.startsWith("L")) {
            currentRotation -= degrees;
            System.out.println(" << " + currentRotation);
        }

        if (currentRotation > 99){
            currentRotation -= 100;
            System.out.println("clamped to " + currentRotation);
        } else if (currentRotation < 0) {
            currentRotation += 100;
            System.out.println("clamped to " + currentRotation);
        }

        if (currentRotation == 0){
            pointsAtZero++;
            System.out.println("points at zero " + pointsAtZero + " times");
        }
    }

    private int processDegrees(String input){
        String degrees = input.substring(1);
        int degreesInt = Integer.parseInt(degrees);
        // remove full rotations
        return degreesInt % 100;
    }
}
