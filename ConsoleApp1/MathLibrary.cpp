#include "pch.h" // use stdafx.h in Visual Studio 2017 and earlier
#include <utility>
#include <limits.h>
#include "MathLibrary.h"
#include <mkl.h>
#include <ctime>
#include <cmath>

void vms_exp(int n, float a[], float y[], float result[], float &max_rel, float& max_pos, float& max_value)
{
    float* y1 = new float[n] {};
    unsigned int start_time = clock();
    vmsExp(n, a, y1, VML_HA);
    unsigned int end_time = clock();
    result[0] = ((float)end_time - start_time) / CLOCKS_PER_SEC;

    float* y2 = new float[n] {};
    start_time = clock();
    vmsExp(n, a, y2, VML_EP);
    end_time = clock();
    result[1] = ((float)end_time - start_time) / CLOCKS_PER_SEC;

    max_rel = 0;
    int pos = 0;
    for (int i = 0; i < n; i++) {
        if (abs(y1[i] - y2[i]) / y1[i] > max_rel) {
            max_rel = abs(y1[i] - y2[i]) / y1[i];
            pos = i;
        }
    }
    max_pos = a[pos];
    max_value = y1[pos];

    start_time = clock();
    for (int i = 0; i < n; i++) {
        exp(a[i]);
    }
    end_time = clock();
    result[2] = ((float)end_time - start_time) / CLOCKS_PER_SEC;
}

void vmd_exp(int n, double a[], double y[], double result[], double& max_rel, double& max_pos, double& max_value)
{
    double* y1 = new double[n] {};
    unsigned int start_time = clock();
    vmdExp(n, a, y1, VML_HA);
    unsigned int end_time = clock();
    result[0] = ((double)end_time - start_time) / CLOCKS_PER_SEC;

    double* y2 = new double[n] {};
    start_time = clock();
    vmdExp(n, a, y2, VML_EP);
    end_time = clock();
    result[1] = ((double)end_time - start_time) / CLOCKS_PER_SEC;

    max_rel = 0;
    int pos = 0;
    for (int i = 0; i < n; i++) {
        if (abs(y1[i] - y2[i]) / y1[i] > max_rel) {
            max_rel = abs(y1[i] - y2[i]) / y1[i];
            pos = i;
        }
    }
    max_pos = a[pos];
    max_value = y1[pos];

    start_time = clock();
    for (int i = 0; i < n; i++) {
        exp(a[i]);
    }
    end_time = clock();
    result[2] = ((double)end_time - start_time) / CLOCKS_PER_SEC;
}