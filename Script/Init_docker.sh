#!/bin/bash
# 사용자 pc에 docker desktop 설치 여부 확인

# 옵션 설명 
# -f (강제 실행)
# up (컨테이너 생성 실행)
# -d (deamon 백그라운드에서 실행)
# --build (이미지 빌드)

# redis
docker-compose -f redis_docker_compose.yml up -d --build

# mariadb
docker-compose -f mariadb_docker_compose.yml up -d --build
