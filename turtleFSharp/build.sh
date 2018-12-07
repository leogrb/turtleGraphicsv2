#!/bin/bash

mono .paket/paket.bootstrapper.exe
mono .paket/paket.exe restore

mono packages/build/Fake/tools/FAKE.exe "build.fsx" $@
