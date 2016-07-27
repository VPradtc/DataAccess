#!/bin/sh
Src="$1"
Dst="$2"
ConfigFile="./zConfig.sh"

set -o pipefail
set -uC
(
	set -ex
	ReplaceCmd="sed -i'' 's/${Src}/${Dst}/g' ${ConfigFile}"

	eval "${ReplaceCmd}"

) 2>&1
ExitCode=$?
(exit ${ExitCode}) && echo 'Success' || echo "Error / ExitCode = $?"