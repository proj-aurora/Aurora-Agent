# Aurora Agent
Agent는 사용자의 호스트에 설치되어 호스트의 상태를 실시간으로 수집할 수 있도록 하는 서비스입니다.

# 사용기술
* Fluent-bit
  * Fluent-bit은 가장 가벼운 로그/Metrics 값 수집기입니다. 이 수집기를 사용하여, `Syslog, Cpu, Disk, Memory` 등등에 대한 상태를 수집하여 Agent에 AICL을 통해 전송합니다.
* Prometheus
  * Prometheus은 오픈소스 모니터링 시스템으로, 다양한 Exporter에서 수집한 정보를 Key-Value 방식으로 제공합니다.
* C#
  * Agent, Bridge(ACL,AICL), Head(Backend)을 개발할 때 사용했습니다. Cross-platform지원 및 Java에 비해 빠른 성능과 낮은 메모리 사용율을 가지는 프로그래밍 언어입니다. Linux와 Windows를 모두 동시에 지원하기 위해 사용했습니다.
* WebSocket
  * ACL, AICL에서 사용했습니다, ACL에서는 Star <-> Agent에서 데이터를 주고받는데 사용했습니다. AICL에서는 Fluent-bit <-> Agent에서 데이터를 보고받는데 사용했습니다.

# 아키텍처
![agent-arch](https://raw.githubusercontent.com/proj-aurora/Aurora-Agent/main/agent-arch.png)

# 흐름도
![agent-flow](https://raw.githubusercontent.com/proj-aurora/Aurora-Agent/main/agent-flow.png)

![image](https://github.com/proj-aurora/Aurora-Agent/assets/33867923/9a3c2ea5-c635-455e-adc3-e41cffede6b6)
